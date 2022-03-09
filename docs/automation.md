





## Use Cases for Browser Automation

In any of the following (and many more) use cases, when the target does not offer a sufficient API, browser automation is the go-to solution:

- Booking a ticket before other bots book them out.
- Replying to an online post programmatically.
- Placing a bet on a live sports-betting site.
- Placing an order on an exchange.
- Timing a bid in an online auction.
- Aggregating a live stream into a dataset.
- Integration testing.


## Reverse Engineering the Target

This is an exploratory stage aiming to figure out a proof-of-concept routine.

Any tools can be used, but it has to be possible to reliably automate the used techniques later. For instance, hand-picking tokens from WebSocket messages to see if they work is fine, the polished agent can intercept WebSocket messages programmatically.

Exploring and rapidly iterating on dead-ends is key. Therefore, this is manual work with little automation potential and Gripper can't be used to speed up the process.



Prior to creating a browser automation agent, the target must be manually reverse-engineered, resulting in a proof-of-concept prototype. The next step is turning the prototype into a reliable agent.

That is where Gripper fits into the workflow.


While the prototype routine may contain manual steps, the agent must be fully automated. Furthermore, every step that involves browser interaction must be expressed in terms of CDP commands.
