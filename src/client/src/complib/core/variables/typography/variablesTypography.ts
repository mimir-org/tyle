import { TypefaceReference, typefaceReference } from "complib/core/variables/typography/reference/typefaceReference";
import { typeScaleReference } from "complib/core/variables/typography/reference/typeScaleReference";
import { body, display, headline, label, title } from "complib/core/variables/typography/system/roles";
import { typeScaleSystem } from "complib/core/variables/typography/system/typeScaleSystem";
import { TypeScaleSpecification, TypographyRoles } from "complib/core/variables/typography/types";
import { css } from "styled-components/macro";

export interface TypographySystem {
  ref: {
    typeface: TypefaceReference,
    typeScale: TypeScaleSpecification<number>
  };
  sys: {
    typeScale: TypeScaleSpecification<string>,
    roles: TypographyRoles
  };
}

export const typography: TypographySystem = {
  ref: {
    typeface: typefaceReference,
    typeScale: typeScaleReference
  },
  sys: {
    typeScale: typeScaleSystem,
    roles: {
      display: display,
      headline: headline,
      title: title,
      body: body,
      label: label
    }
  }
};

export const variablesTypography = css`
  :root {
    --tl-ref-typeface-brand: "roboto", sans-serif;

    --tl-ref-typeface-weight-bold: ${typography.ref.typeface.weights.bold};
    --tl-ref-typeface-weight-medium: ${typography.ref.typeface.weights.medium};
    --tl-ref-typeface-weight-normal: ${typography.ref.typeface.weights.normal};
    --tl-ref-typeface-weight-light: ${typography.ref.typeface.weights.light};
    --tl-ref-font-base-size: 100%;

    // Font sizes in PX
    --tl-ref-typescale-size-b: ${typography.ref.typeScale.size.base};
    --tl-ref-typescale-size-n3: ${typography.ref.typeScale.size.n3};
    --tl-ref-typescale-size-n2: ${typography.ref.typeScale.size.n2};
    --tl-ref-typescale-size-n1: ${typography.ref.typeScale.size.n1};
    --tl-ref-typescale-size-p1: ${typography.ref.typeScale.size.p1};
    --tl-ref-typescale-size-p2: ${typography.ref.typeScale.size.p2};
    --tl-ref-typescale-size-p3: ${typography.ref.typeScale.size.p3};
    --tl-ref-typescale-size-p4: ${typography.ref.typeScale.size.p4};
    --tl-ref-typescale-size-p5: ${typography.ref.typeScale.size.p5};
    --tl-ref-typescale-size-p6: ${typography.ref.typeScale.size.p6};
    --tl-ref-typescale-size-p7: ${typography.ref.typeScale.size.p7};

    // Line heights in PX
    --tl-ref-typescale-line-height-b: ${typography.ref.typeScale.lineHeight.base};
    --tl-ref-typescale-line-height-n3: ${typography.ref.typeScale.lineHeight.n3};
    --tl-ref-typescale-line-height-n2: ${typography.ref.typeScale.lineHeight.n2};
    --tl-ref-typescale-line-height-n1: ${typography.ref.typeScale.lineHeight.n1};
    --tl-ref-typescale-line-height-p1: ${typography.ref.typeScale.lineHeight.p1};
    --tl-ref-typescale-line-height-p2: ${typography.ref.typeScale.lineHeight.p2};
    --tl-ref-typescale-line-height-p3: ${typography.ref.typeScale.lineHeight.p3};
    --tl-ref-typescale-line-height-p4: ${typography.ref.typeScale.lineHeight.p4};
    --tl-ref-typescale-line-height-p5: ${typography.ref.typeScale.lineHeight.p5};
    --tl-ref-typescale-line-height-p6: ${typography.ref.typeScale.lineHeight.p6};
    --tl-ref-typescale-line-height-p7: ${typography.ref.typeScale.lineHeight.p7};

    // REM converted font sizes
    --tl-sys-typescale-size-b: ${typography.sys.typeScale.size.base};
    --tl-sys-typescale-size-n3: ${typography.sys.typeScale.size.n3};
    --tl-sys-typescale-size-n2: ${typography.sys.typeScale.size.n2};
    --tl-sys-typescale-size-n1: ${typography.sys.typeScale.size.n1};
    --tl-sys-typescale-size-p1: ${typography.sys.typeScale.size.p1};
    --tl-sys-typescale-size-p2: ${typography.sys.typeScale.size.p2};
    --tl-sys-typescale-size-p3: ${typography.sys.typeScale.size.p3};
    --tl-sys-typescale-size-p4: ${typography.sys.typeScale.size.p4};
    --tl-sys-typescale-size-p5: ${typography.sys.typeScale.size.p5};
    --tl-sys-typescale-size-p6: ${typography.sys.typeScale.size.p6};
    --tl-sys-typescale-size-p7: ${typography.sys.typeScale.size.p7};

    // REM converted line heights
    --tl-sys-typescale-line-height-b: ${typography.sys.typeScale.lineHeight.base};
    --tl-sys-typescale-line-height-n3: ${typography.sys.typeScale.lineHeight.n3};
    --tl-sys-typescale-line-height-n2: ${typography.sys.typeScale.lineHeight.n2};
    --tl-sys-typescale-line-height-n1: ${typography.sys.typeScale.lineHeight.n1};
    --tl-sys-typescale-line-height-p1: ${typography.sys.typeScale.lineHeight.p1};
    --tl-sys-typescale-line-height-p2: ${typography.sys.typeScale.lineHeight.p2};
    --tl-sys-typescale-line-height-p3: ${typography.sys.typeScale.lineHeight.p3};
    --tl-sys-typescale-line-height-p4: ${typography.sys.typeScale.lineHeight.p4};
    --tl-sys-typescale-line-height-p5: ${typography.sys.typeScale.lineHeight.p5};
    --tl-sys-typescale-line-height-p6: ${typography.sys.typeScale.lineHeight.p6};
    --tl-sys-typescale-line-height-p7: ${typography.sys.typeScale.lineHeight.p7};

    // Display types
    --tl-sys-typescale-display-large: ${typography.sys.roles.display.large.font};
    --tl-sys-typescale-display-large-font: ${typography.sys.roles.display.large.family};
    --tl-sys-typescale-display-large-size: ${typography.sys.roles.display.large.size};
    --tl-sys-typescale-display-large-weight: ${typography.sys.roles.display.large.weight};
    --tl-sys-typescale-display-large-tracking: ${typography.sys.roles.display.large.tracking};
    --tl-sys-typescale-display-large-spacing: ${typography.sys.roles.display.large.letterSpacing};
    --tl-sys-typescale-display-large-line-height: ${typography.sys.roles.display.large.lineHeight};

    --tl-sys-typescale-display-medium: ${typography.sys.roles.display.medium.font};
    --tl-sys-typescale-display-medium-font: ${typography.sys.roles.display.medium.family};
    --tl-sys-typescale-display-medium-size: ${typography.sys.roles.display.medium.size};
    --tl-sys-typescale-display-medium-weight: ${typography.sys.roles.display.medium.weight};
    --tl-sys-typescale-display-medium-tracking: ${typography.sys.roles.display.medium.tracking};
    --tl-sys-typescale-display-medium-spacing: ${typography.sys.roles.display.medium.letterSpacing};
    --tl-sys-typescale-display-medium-line-height: ${typography.sys.roles.display.medium.lineHeight};

    --tl-sys-typescale-display-small: ${typography.sys.roles.display.small.font};
    --tl-sys-typescale-display-small-font: ${typography.sys.roles.display.small.family};
    --tl-sys-typescale-display-small-size: ${typography.sys.roles.display.small.size};
    --tl-sys-typescale-display-small-weight: ${typography.sys.roles.display.small.weight};
    --tl-sys-typescale-display-small-tracking: ${typography.sys.roles.display.small.tracking};
    --tl-sys-typescale-display-small-spacing: ${typography.sys.roles.display.small.letterSpacing};
    --tl-sys-typescale-display-small-line-height: ${typography.sys.roles.display.small.lineHeight};

    // Headline types
    --tl-sys-typescale-headline-large: ${typography.sys.roles.headline.large.font};
    --tl-sys-typescale-headline-large-font: ${typography.sys.roles.headline.large.family};
    --tl-sys-typescale-headline-large-size: ${typography.sys.roles.headline.large.size};
    --tl-sys-typescale-headline-large-weight: ${typography.sys.roles.headline.large.weight};
    --tl-sys-typescale-headline-large-tracking: ${typography.sys.roles.headline.large.tracking};
    --tl-sys-typescale-headline-large-spacing: ${typography.sys.roles.headline.large.letterSpacing};
    --tl-sys-typescale-headline-large-line-height: ${typography.sys.roles.headline.large.lineHeight};

    --tl-sys-typescale-headline-medium: ${typography.sys.roles.headline.medium.font};
    --tl-sys-typescale-headline-medium-font: ${typography.sys.roles.headline.medium.family};
    --tl-sys-typescale-headline-medium-size: ${typography.sys.roles.headline.medium.size};
    --tl-sys-typescale-headline-medium-weight: ${typography.sys.roles.headline.medium.weight};
    --tl-sys-typescale-headline-medium-tracking: ${typography.sys.roles.headline.medium.tracking};
    --tl-sys-typescale-headline-medium-spacing: ${typography.sys.roles.headline.medium.letterSpacing};
    --tl-sys-typescale-headline-medium-line-height: ${typography.sys.roles.headline.medium.lineHeight};

    --tl-sys-typescale-headline-small: ${typography.sys.roles.headline.small.font};
    --tl-sys-typescale-headline-small-font: ${typography.sys.roles.headline.small.family};
    --tl-sys-typescale-headline-small-size: ${typography.sys.roles.headline.small.size};
    --tl-sys-typescale-headline-small-weight: ${typography.sys.roles.headline.small.weight};
    --tl-sys-typescale-headline-small-tracking: ${typography.sys.roles.headline.small.tracking};
    --tl-sys-typescale-headline-small-spacing: ${typography.sys.roles.headline.small.letterSpacing};
    --tl-sys-typescale-headline-small-line-height: ${typography.sys.roles.headline.small.lineHeight};

    // Title types
    --tl-sys-typescale-title-large: ${typography.sys.roles.title.large.font};
    --tl-sys-typescale-title-large-font: ${typography.sys.roles.title.large.family};
    --tl-sys-typescale-title-large-size: ${typography.sys.roles.title.large.size};
    --tl-sys-typescale-title-large-weight: ${typography.sys.roles.title.large.weight};
    --tl-sys-typescale-title-large-tracking: ${typography.sys.roles.title.large.tracking};
    --tl-sys-typescale-title-large-spacing: ${typography.sys.roles.title.large.letterSpacing};
    --tl-sys-typescale-title-large-line-height: ${typography.sys.roles.title.large.lineHeight};

    --tl-sys-typescale-title-medium: ${typography.sys.roles.title.medium.font};
    --tl-sys-typescale-title-medium-font: ${typography.sys.roles.title.medium.family};
    --tl-sys-typescale-title-medium-size: ${typography.sys.roles.title.medium.size};
    --tl-sys-typescale-title-medium-weight: ${typography.sys.roles.title.medium.weight};
    --tl-sys-typescale-title-medium-tracking: ${typography.sys.roles.title.medium.tracking};
    --tl-sys-typescale-title-medium-spacing: ${typography.sys.roles.title.medium.letterSpacing};
    --tl-sys-typescale-title-medium-line-height: ${typography.sys.roles.title.medium.lineHeight};

    --tl-sys-typescale-title-small: ${typography.sys.roles.title.small.font};
    --tl-sys-typescale-title-small-font: ${typography.sys.roles.title.small.family};
    --tl-sys-typescale-title-small-size: ${typography.sys.roles.title.small.size};
    --tl-sys-typescale-title-small-weight: ${typography.sys.roles.title.small.weight};
    --tl-sys-typescale-title-small-tracking: ${typography.sys.roles.title.small.tracking};
    --tl-sys-typescale-title-small-spacing: ${typography.sys.roles.title.small.letterSpacing};
    --tl-sys-typescale-title-small-line-height: ${typography.sys.roles.title.small.lineHeight};

    // Body types
    --tl-sys-typescale-body-large: ${typography.sys.roles.body.large.font};
    --tl-sys-typescale-body-large-font: ${typography.sys.roles.body.large.family};
    --tl-sys-typescale-body-large-size: ${typography.sys.roles.body.large.size};
    --tl-sys-typescale-body-large-weight: ${typography.sys.roles.body.large.weight};
    --tl-sys-typescale-body-large-tracking: ${typography.sys.roles.body.large.tracking};
    --tl-sys-typescale-body-large-spacing: ${typography.sys.roles.body.large.letterSpacing};
    --tl-sys-typescale-body-large-line-height: ${typography.sys.roles.body.large.lineHeight};

    --tl-sys-typescale-body-medium: ${typography.sys.roles.body.medium.font};
    --tl-sys-typescale-body-medium-font: ${typography.sys.roles.body.medium.family};
    --tl-sys-typescale-body-medium-size: ${typography.sys.roles.body.medium.size};
    --tl-sys-typescale-body-medium-weight: ${typography.sys.roles.body.medium.weight};
    --tl-sys-typescale-body-medium-tracking: ${typography.sys.roles.body.medium.tracking};
    --tl-sys-typescale-body-medium-spacing: ${typography.sys.roles.body.medium.letterSpacing};
    --tl-sys-typescale-body-medium-line-height: ${typography.sys.roles.body.medium.lineHeight};

    --tl-sys-typescale-body-small: ${typography.sys.roles.body.small.font};
    --tl-sys-typescale-body-small-font: ${typography.sys.roles.body.small.family};
    --tl-sys-typescale-body-small-size: ${typography.sys.roles.body.small.size};
    --tl-sys-typescale-body-small-weight: ${typography.sys.roles.body.small.weight};
    --tl-sys-typescale-body-small-tracking: ${typography.sys.roles.body.small.tracking};
    --tl-sys-typescale-body-small-spacing: ${typography.sys.roles.body.small.letterSpacing};
    --tl-sys-typescale-body-small-line-height: ${typography.sys.roles.body.small.lineHeight};

    // Label types
    --tl-sys-typescale-label-large: ${typography.sys.roles.label.large.font};
    --tl-sys-typescale-label-large-font: ${typography.sys.roles.label.large.family};
    --tl-sys-typescale-label-large-size: ${typography.sys.roles.label.large.size};
    --tl-sys-typescale-label-large-weight: ${typography.sys.roles.label.large.weight};
    --tl-sys-typescale-label-large-tracking: ${typography.sys.roles.label.large.tracking};
    --tl-sys-typescale-label-large-spacing: ${typography.sys.roles.label.large.letterSpacing};
    --tl-sys-typescale-label-large-line-height: ${typography.sys.roles.label.large.lineHeight};

    --tl-sys-typescale-label-medium: ${typography.sys.roles.label.medium.font};
    --tl-sys-typescale-label-medium-font: ${typography.sys.roles.label.medium.family};
    --tl-sys-typescale-label-medium-size: ${typography.sys.roles.label.medium.size};
    --tl-sys-typescale-label-medium-weight: ${typography.sys.roles.label.medium.weight};
    --tl-sys-typescale-label-medium-tracking: ${typography.sys.roles.label.medium.tracking};
    --tl-sys-typescale-label-medium-spacing: ${typography.sys.roles.label.medium.letterSpacing};
    --tl-sys-typescale-label-medium-line-height: ${typography.sys.roles.label.medium.lineHeight};

    --tl-sys-typescale-label-small: ${typography.sys.roles.label.small.font};
    --tl-sys-typescale-label-small-font: ${typography.sys.roles.label.small.family};
    --tl-sys-typescale-label-small-size: ${typography.sys.roles.label.small.size};
    --tl-sys-typescale-label-small-weight: ${typography.sys.roles.label.small.weight};
    --tl-sys-typescale-label-small-tracking: ${typography.sys.roles.label.small.tracking};
    --tl-sys-typescale-label-small-spacing: ${typography.sys.roles.label.small.letterSpacing};
    --tl-sys-typescale-label-small-line-height: ${typography.sys.roles.label.small.lineHeight};
  }
`;