# Research: State, Caching, and Cart

## Decision: Clearing Filter State
**Decision**: Utilize `OnParametersSetAsync` in `Category.razor` to detect when the `Slug` parameter changes. If it changes to a new primary category, reset `_maxPrice`, `_selectedColor`, `_sortBy`, and `_selectedSubcategorySlug` to their defaults before fetching products.
**Rationale**: `OnParametersSetAsync` is the Blazor lifecycle method specifically designed to handle route parameter changes within the same component instance.
**Alternatives considered**: Forcing a full page reload (bad for Blazor Server UX).

## Decision: In-Memory Caching Implementation
**Decision**: Inject `IMemoryCache` into `ProductService`. Wrap existing Dapper repository calls with `_cache.GetOrCreateAsync`. Use an absolute expiration time of 15 minutes.
**Rationale**: `IMemoryCache` is the built-in, thread-safe, and standard way to cache objects in a Blazor Server app without requiring external infrastructure like Redis.
**Alternatives considered**: Static dictionaries (not thread-safe, prone to memory leaks without manual eviction).

## Decision: Cart Page Layout
**Decision**: Create `Cart.razor` using a mobile-first `MudGrid`. Display a `MudTable` or stacked `MudCards` (on mobile) of cart items, with a summary section for the total price and checkout button.
**Rationale**: Must adhere to Principle 29 (Mobile-First Page Design) and replace the existing 404 error.
**Alternatives considered**: N/A - A functional cart page is a hard requirement.
