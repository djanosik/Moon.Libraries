# Moon.Libraries changelog

3.0.1

- All libraries are targeting .NET Standard.
- `PasswordHash` has been renamed to `PBKDF2` to make it clear what algorithm is used.
- Added `BCrypt` helper as an alternative to `PBKDF2` for password hashing.
- Added `TOTP` helper generating one-time passwords compatible with several authenticator apps.
- Removed `IEnumerable<T>.ForEach` extension method.

2.2.8

- Added `Gravatar` helper returning global avatar URLs for e-mail addresses.
- `Anonymous` helper properly handles objects of type `IDictionary<string, object>`.
- Packages don't have unnecesary dependencies anymore.