using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ImGuiNET;

namespace RDR2CS
{
    public static class NotificationConstants
    {
        public const float CardSizeX = 350f;
        public const float CardSizeY = 100f;
        public const float CardAnimationSpeed = 50f;
    }

    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Error
    }

    public class Notification
    {
        public NotificationType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public TimeSpan Duration { get; set; }
        public Action ContextFunction { get; set; }
        public string ContextFunctionName { get; set; }
        public float AnimationOffset { get; set; }
        public bool Erasing { get; set; }

        public Notification(NotificationType type, string title, string message, TimeSpan duration, Action contextFunction = null, string contextFunctionName = "")
        {
            Type = type;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            CreatedOn = DateTime.UtcNow;
            Duration = duration;
            ContextFunction = contextFunction;
            ContextFunctionName = contextFunctionName;
            AnimationOffset = -NotificationConstants.CardSizeX;
            Erasing = false;
        }

        public string GetIdentifier() => $"{Title}{Message}";
    }

    public sealed class Notifications
    {
        private readonly ConcurrentDictionary<string, Notification> _notifications = new ConcurrentDictionary<string, Notification>();
        private readonly object _lock = new object();

        private static readonly Lazy<Notifications> _instance = 
            new Lazy<Notifications>(() => new Notifications(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        public static Notifications Instance => _instance.Value;

        private Notifications() { }

        public static Notification Show(string title, string message, NotificationType type = NotificationType.Info, TimeSpan? duration = null, Action contextFunction = null, string contextFunctionName = "")
        {
            return Instance.ShowImpl(title, message, type, duration, contextFunction, contextFunctionName);
        }

        
        private Notification ShowImpl(string title, string message, NotificationType type, TimeSpan? duration, Action contextFunction, string contextFunctionName)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or whitespace.", nameof(title));
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be null or whitespace.", nameof(message));

            var notification = new Notification(type, title, message, duration ?? TimeSpan.FromSeconds(5), contextFunction, contextFunctionName);

            if (_notifications.ContainsKey(notification.GetIdentifier()))
            {
                return null;
            }

            if (contextFunction != null)
            {
                notification.ContextFunction = contextFunction;
                notification.ContextFunctionName = string.IsNullOrEmpty(contextFunctionName) ? "Context Function" : contextFunctionName;
            }

            _notifications.TryAdd(notification.GetIdentifier(), notification);
            return notification;
        }

        
        public static bool Erase(Notification notification)
        {
            return Instance.EraseImpl(notification);
        }

        
        private bool EraseImpl(Notification notification)
        {
            if (_notifications.TryGetValue(notification.GetIdentifier(), out var existingNotification))
            {
                existingNotification.Erasing = true;
                return true;
            }
            return false;
        }

        
        static void DrawNotification(Notification notification, int position)
        {
            float yPos = position * 100;
            float xPos = 10;
            Vector2 cardSize = new Vector2(NotificationConstants.CardSizeX, NotificationConstants.CardSizeY);
            Vector2 cardPos = new Vector2(xPos + notification.AnimationOffset, yPos + 10);

            ImGui.SetNextWindowSize(cardSize, ImGuiCond.Always);
            ImGui.SetNextWindowPos(cardPos, ImGuiCond.Always);

            string windowTitle = $"Notification {position + 1}";
            ImGui.Begin(windowTitle, ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize);

            float timeElapsed = (float)(DateTime.UtcNow - notification.CreatedOn).TotalMilliseconds;
            float depletionProgress = 1.0f - (timeElapsed / (float)notification.Duration.TotalMilliseconds);

            ImGui.ProgressBar(depletionProgress, new Vector2(-1, 1), "");
            
            switch (notification.Type)
            {
                case NotificationType.Info:
                    ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 1.0f, 1.0f, 1.0f));
                    ImGui.Text(notification.Title);
                    break;
                case NotificationType.Success:
                    ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(0.0f, 1.0f, 0.0f, 1.0f));
                    ImGui.Text(notification.Title);
                    break;
                case NotificationType.Warning:
                    ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 0.5f, 1.0f, 1.0f));
                    ImGui.Text(notification.Title);
                    break;
                case NotificationType.Error:
                    ImGui.PushStyleColor(ImGuiCol.Text, new Vector4(1.0f, 0.0f, 0.0f, 1.0f));
                    ImGui.Text(notification.Title);
                    break;
            }
            
            ImGui.PopStyleColor();
            ImGui.Separator();
            ImGui.TextWrapped(notification.Message);
            // if (notification.ContextFunction != null)
            // {
            //     ImGui.Spacing();
            //     if (ImGui.Selectable(notification.ContextFunctionName))
            //     {
            //         // import FiberPool from wrapper
            //         FiberPool.Push([notification]
            //         {
            //             notification.ContextFunction();
            //         });
            //     }
            // }
            ImGui.End();
        }

        public static void Draw()
        {
            Instance.DrawImpl();
        }

        private void DrawImpl()
        {
            List<string> keysToErase = new List<string>();
            int position = 0;

            foreach (var kvp in _notifications)
            {
                var notification = kvp.Value;
                DrawNotification(notification, position);

                if (!notification.Erasing)
                {
                    if (notification.AnimationOffset < 0)
                        notification.AnimationOffset += NotificationConstants.CardAnimationSpeed;

                    if (notification.AnimationOffset > 0)
                        notification.AnimationOffset = 0f;
                }
                else
                {
                    notification.AnimationOffset -= NotificationConstants.CardAnimationSpeed;
                    if (notification.AnimationOffset <= -NotificationConstants.CardSizeX)
                        keysToErase.Add(kvp.Key);
                }

                if ((DateTime.UtcNow - notification.CreatedOn) >= notification.Duration)
                    keysToErase.Add(kvp.Key);

                position++;
            }

            foreach (var key in keysToErase)
            {
                _notifications.TryRemove(key, out _);
            }
        }
    }
}