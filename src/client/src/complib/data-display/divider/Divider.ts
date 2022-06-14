import styled from "styled-components";
import * as Separator from "@radix-ui/react-separator";

interface DividerProps {
  orientation?: "horizontal" | "vertical";
  decorative?: boolean;
}

/**
 * A simple divider for creating a clear separation between content
 */
export const Divider = styled(Separator.Root)<DividerProps>`
  background-color: ${(props) => props.theme.tyle.color.outline.base};
  margin: 0 auto;
  height: 2px;
  width: 90%;

  ${(props) =>
    props.orientation === "vertical" &&
    `
    margin: auto 0;
    height: 90%;
    width: 2px;
  `}
`;

Divider.defaultProps = {
  orientation: "horizontal",
  decorative: false,
};
