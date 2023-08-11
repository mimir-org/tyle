# Variables

## Purpose

This directory houses variables/tokens that makes up a "TyleTheme". 
The structures and their naming are influenced by Material Design 3.

## Using variables
As these variables are a part of a larger theme you do not consume them directly, but access them via the theme object.  
The theme object can be retrieved via the useTheme hook or directly when inside a styled component - see examples below.

Most variables are also exposed as CSS variables so that applications that want to utilize the :TYLE styling can do so without using CSS-in-JS.  
To make both the theme and css variables available you can wrap the application with the "TyleThemeProvider.tsx".

___Tip: Do not consume the theme-object found within core/theme/theme.ts directly as this NOT will respect changes to theme preferences such as dark mode.___

### Example usage JSX

```jsx
export const Styled = () => {
  const theme = useTheme();

  return (
    <Box 
      bgColor={theme.mimirorg.color.primary.base} 
      color={theme.mimirorg.color.primary.on} 
      borderRadius={theme.mimirorg.border.radius.large}
      p={theme.mimirorg.spacing.xl} 
    >
      <Text>This component consumes theme styles via hook</Text>
    </Box>
  );
};
```

### Example usage TS (styled-component)

```ts
// Consumes theme via auto-injected theme property
export const StyledComponent = styled.div`
  background-color: ${(props) => props.theme.mimirorg.color.primary.base};
  color: ${(props) => props.theme.mimirorg.color.primary.on};
  border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
  padding: ${(props) => props.theme.mimirorg.spacing.xl};
`;
```
