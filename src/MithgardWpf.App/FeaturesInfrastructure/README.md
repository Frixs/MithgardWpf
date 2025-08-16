### I’m the placeholder for the best infrastructure logic you've ever created.
**Remember:** Whenever you are unsure where to place code that is shared across multiple features, ask yourself whether it belongs to **business logic** or **infrastructure**.  
- If it is **business logic**, keep it inside each feature, even if it means some code duplication ([DRY](https://en.wikipedia.org/wiki/Don%27t_repeat_yourself)).  
- If it is **infrastructure**, place it here, define a contract in `FeaturesShared`, and then use it in the features that need it.  
