import { css } from "styled-components/macro";

export const variablesColor = css`
  :root {
    --color-ui-primary-hue: 240;
    --color-ui-primary: hsl(var(--color-ui-primary-hue), 17%, 19%);
    --color-ui-primary-light: hsl(var(--color-ui-primary-hue), 17%, 26%);
    --color-ui-primary-dark: hsl(var(--color-ui-primary-hue), 17%, 15%);
    --color-ui-primary-alpha: hsla(var(--color-ui-primary-hue), 17%, 19%, 0.5);

    --color-ui-secondary: hsl(225, 100%, 94%);
    --color-ui-secondary-dark: hsl(240, 34%, 89%);

    --color-ui-ok: hsl(121, 100%, 72%);
    --color-ui-warn: hsl(40, 100%, 72%);
    --color-ui-danger: hsl(349, 100%, 72%);
    --color-ui-danger-dark: hsl(349, 100%, 65%);

    --color-grey-scale-1: hsl(0, 0%, 98%);
    --color-grey-scale-2: hsl(0, 0%, 88%);
    --color-grey-scale-3: hsl(0, 0%, 77%);
    --color-grey-scale-4: hsl(0, 0%, 55%);
    --color-grey-scale-5: hsl(0, 0%, 31%);

    --color-text-primary: hsl(0, 0%, 0%);
    --color-text-secondary: hsl(0, 0%, 55%);

    --color-neutral-light: hsl(0, 0%, 100%);
    --color-neutral-dark: hsl(0, 0%, 0%);

    --color-function-hue: 58;
    --color-function-primary: hsl(var(--color-function-hue), 95%, 64%);
    --color-function-dark: hsl(calc(var(--color-function-hue) - 8), 84%, 52%);
    --color-function-light: hsl(calc(var(--color-function-hue) - 8), 85%, 72%);
    --color-function-lighter: hsl(var(--color-function-hue), 95%, 83%);

    --color-product-hue: 184;
    --color-product-primary: hsl(var(--color-product-hue), 100%, 50%);
    --color-product-dark: hsl(var(--color-product-hue), 92%, 31%);
    --color-product-light: hsl(var(--color-product-hue), 76%, 59%);
    --color-product-lighter: hsl(var(--color-product-hue), 84%, 85%);

    --color-location-hue: 299;
    --color-location-primary: hsl(var(--color-location-hue), 100%, 50%);
    --color-location-dark: hsl(var(--color-location-hue), 100%, 33%);
    --color-location-light: hsl(var(--color-location-hue), 80%, 73%);
    --color-location-lighter: hsl(var(--color-location-hue), 96%, 90%);
  }
`;
