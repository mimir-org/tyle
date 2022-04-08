import { TextContainer } from "./Text.styled";
import { ElementType, PropsWithChildren } from "react";
import { Typography } from "../props";

export interface TextProps<T extends ElementType> extends Pick<Typography, "font" | "fontSize"> {
  as?: T;
  useEllipsis?: boolean;
  ellipsisMaxLines?: number;
}

/**
 * A polymorphic text component for non-heading text
 *
 * @param as element to display component as (defaults to <p>)
 * @param font overrides font of text element
 * @param fontSize overrides default size of text element
 * @param useEllipsis enable truncation of text
 * @param ellipsisMaxLines set how many lines to display before truncation
 * @param children content of text element
 * @constructor
 */
export function Text<T extends ElementType>({
  as,
  font,
  fontSize,
  useEllipsis = false,
  ellipsisMaxLines = 1,
  children,
}: PropsWithChildren<TextProps<T>>) {
  const Component = as || "p";
  return (
    <TextContainer
      as={Component}
      font={font}
      fontSize={fontSize}
      useEllipsis={useEllipsis}
      ellipsisMaxLines={ellipsisMaxLines}
    >
      {children}
    </TextContainer>
  );
}
