import styled, { css } from "styled-components/macro";
import { sizingMixin } from "../../mixins";
import { AccordionProps } from "./Accordion";

export const StyledAccordionRoot = styled.div<Pick<AccordionProps, "type">>`
  display: flex;
  flex-direction: column;

  ${({ type, ...props }) =>
    type === "multiple" &&
    css`
      gap: ${props.theme.tyle.spacing.base};
    `}

  > [data-state="open"] {
    border: 1px solid ${(props) => props.theme.tyle.color.sys.secondary.base};
    background-color: ${(props) => props.theme.tyle.color.sys.secondary.container?.base};
  }

  ${sizingMixin};
`;
