public record ProductRequest
(
    string Code, string Mark, string Amount, int CategoryId, List<string> Tags
);
