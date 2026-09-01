# Data Model: State, Caching, and Cart

## Relevant Existing Entities
- **CategoryDto**: Will be cached in memory as a hierarchical list.
- **ProductDto**: Lists of products will be cached by category slug and filter parameters.
- **CartItemDto**: Represents items currently in the user's cart (managed by `ICartService`).

No new schema changes are required. The focus is entirely on caching existing entities and presenting the existing `CartItemDto` models in a new UI.
