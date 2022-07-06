import styled from "styled-components/macro";
import { sizingMixin } from "../../mixins";
import { AccordionProps } from "./Accordion";

export const StyledAccordionRoot = styled.div<Pick<AccordionProps, "type">>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.type === "multiple" ? props.theme.tyle.spacing.base : props.theme.tyle.spacing.xs)};
  ${sizingMixin};
`;
