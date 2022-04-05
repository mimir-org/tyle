import styled from "styled-components/macro";

interface FlexProps {
  gap?: string;
  flexDirection?: "column" | "row";
  alignItems?: string;
  justifyContent?: string;
}

export const Flex = styled.div<FlexProps>`
  display: flex;
  gap: ${(props) => props.gap};
  flex-direction: ${(props) => props.flexDirection};
  align-items: ${(props) => props.alignItems};
  justify-content: ${(props) => props.justifyContent};
`;

Flex.defaultProps = {
  gap: "var(--spacing-small)",
};
