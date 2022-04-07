import styled from "styled-components/macro";

interface FlexProps {
  gap?: string;
  flexDirection?: "column" | "row";
  alignItems?: string;
  justifyContent?: string;
  padding?: string;
  border?: string;
  borderRadius?: string;
  boxShadow?: string;
  width?: string;
  height?: string;
}

export const Flex = styled.div<FlexProps>`
  display: flex;
  gap: ${(props) => props.gap};
  flex-direction: ${(props) => props.flexDirection};
  align-items: ${(props) => props.alignItems};
  justify-content: ${(props) => props.justifyContent};

  width: ${(props) => props.width};
  height: ${(props) => props.height};

  padding: ${(props) => props.padding};
  border: ${(props) => props.border};
  border-radius: ${(props) => props.borderRadius};

  box-shadow: ${(props) => props.boxShadow};
`;

Flex.defaultProps = {
  gap: "var(--spacing-small)",
};
