import { HeadingContainer } from "./Heading.styled";
import { ElementType, PropsWithChildren } from "react";
import { Typography } from "../props";

export interface HeadingProps<T extends ElementType> extends Pick<Typography, "font" | "fontSize"> {
  as?: T;
  useEllipsis?: boolean;
  ellipsisMaxLines?: number;
}

/**
 * A polymorphic component for heading elements
 *
 * @param as element to display component as (defaults to <p>)
 * @param font overrides font of heading element
 * @param fontSize overrides default size of heading element
 * @param useEllipsis enable truncation of text
 * @param ellipsisMaxLines set how many lines to display before truncation
 * @param children content of text element
 * @constructor
 */
export function Heading<T extends ElementType>({
  as,
  font,
  fontSize,
  useEllipsis = false,
  ellipsisMaxLines = 1,
  children,
}: PropsWithChildren<HeadingProps<T>>) {
  const Component = as || "h1";
  return (
    <HeadingContainer
      as={Component}
      font={font}
      fontSize={fontSize}
      useEllipsis={useEllipsis}
      ellipsisMaxLines={ellipsisMaxLines}
    >
      {children}
    </HeadingContainer>
  );
}
