import styled from "styled-components";

interface DividerProps {
  variant?: "horizontal" | "vertical";
}

/**
 * A simple divider for creating a clear separation between content
 */
export const Divider = styled.hr<DividerProps>`
  color: ${(props) => props.theme.typeLibrary.color.outline.base};
  margin: 0 auto;
  height: 2px;
  width: 90%;

  ${(props) =>
    props.variant === "vertical" &&
    `
    margin: auto 0;
    height: 90%;
    width: 2px;
  `}
`;

Divider.defaultProps = {
  variant: "horizontal",
};
