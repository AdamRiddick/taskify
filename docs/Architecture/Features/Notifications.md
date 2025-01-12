# Overview

Notifications within Taskify are handled using the Outbox pattern.

Taskify.Infrastructure.Notifications.Dispatcher handles creation of notification entries, and is called via domains. This is triggered using Mediatr, and sending a SendNotificationCommand.

Notifications call into one or three types, and can be sent to one or more channels, dependent on a users notification preferences. The rules allowed for each notification type are defined below.

## Notification Types

| Type  | Default  | Action |
| ----- | -------- | -------------- |
| Marketing | Disabled | Can Opt-In |
| System    | Enabled  | Can Opt-Out|
| Security  | Enabled  | No Opt-Out |

## Notification Sending

Coming soon to a Taskify near you.
