# Supported Slack message events

This page lists [Slack message events] supported by Usain.

> Some message event types are _partially supported_ because Blocks are not yet deserialized.

Event | Descriminator | Description | Supported | Comments
------------ | ------------- | ------------- | ------------- | -------------
[message](https://api.slack.com/events/message) | Main type | A message was sent to a channel | :large_blue_circle: | Attachements not supported
[bot_message](https://api.slack.com/events/message/bot_message) | Subtype | A message was posted by an integration | :red_circle: |
[ekm_access_denied](https://api.slack.com/events/message/ekm_access_denied) | Subtype | Message content redacted due to Enterprise Key Management (EKM) | :red_circle: |
[me_message](https://api.slack.com/events/message/me_message) | Subtype | A /me message was sent | :white_check_mark: |
[message_changed](https://api.slack.com/events/message/message_changed) | Subtype | A message was changed | :large_blue_circle: | Attachements not supported
[message_deleted](https://api.slack.com/events/message/message_deleted) | Subtype | A message was deleted | :large_blue_circle: | Attachements not supported
[message_replied](https://api.slack.com/events/message/message_replied) | Subtype | A message thread received a reply | :large_blue_circle: | Attachements not supported
[thread_broadcast](https://api.slack.com/events/message/thread_broadcast) | Subtype | A message thread's reply was broadcast to a channel | :red_circle: |

:white_check_mark: Supported\
:large_blue_circle: Partially supported\
:red_circle: Not supported

[Slack message events]: <https://api.slack.com/events/message#message_subtypes>
