# Locales

## Purpose

This directory houses text resources that are used throughout the application via https://react.i18next.com/.  
The i18next library enables us to localize the application in a structured manner and comes with benefits such as
template strings, splitting of translation files etc.

## Rules

- Every language resource must reside within its own directory e.g. en, no.

## Example usage

Example translation file __"{someDirectory}/locales/en/translation.json"__

```json
{
  "user": {
    "title": "Account",
    "subtitle": "Your account information"
  }
}
```

Example translation file __"{someDirectory}/locales/no/translation.json"__

```json
{
  "user": {
    "title": "Konto",
    "subtitle": "Din konto informasjon"
  }
}
```

```jsx
  // Via i18next autodetection or by offering a language picker;
  // this component is now able to resolve which text resource to use automatically.
  export const MyLocalizedComponent = () => {
      const { t } = useTranslation();
    
      return (
        <div>        
          <h1>{t("user.title")}</h1>
          <h2>{t("user.subtitle")}</h2>
        </div>
      );
  }; 
```

```jsx
  // You can often shorten the localization tokens by using a prefix as shown below.
  // The first parameter select a namespace, "translation" is the default.
  export const MyLocalizedComponent = () => {
      const { t } = useTranslation("translation", { keyPrefix: "user" });
    
      return (
        <div>        
          <h1>{t("title")}</h1>
          <h2>{t("subtitle")}</h2>
        </div>
      );
  }; 
```

In addition to what is shown above there are more ways to translate your components, make sure to checkout https://react.i18next.com/guides/quick-start for more examples.