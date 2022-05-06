type DisplayType = "display-large" | "display-medium" | "display-small";

type HeadlineType = "headline-large" | "headline-medium" | "headline-small";

type TitleType = "title-large" | "title-medium" | "title-small";

type BodyType = "body-large" | "body-medium" | "body-small";

type LabelType = "label-large" | "label-medium" | "label-small";

export type TextTypes = DisplayType | HeadlineType | TitleType | BodyType | LabelType;

export interface TextVariant {
  variant?: TextTypes;
}
