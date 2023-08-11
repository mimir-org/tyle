import { sizingMixin } from "complib/mixins";
import { AccordionProps } from "complib/surfaces/accordion/Accordion";
import styled from "styled-components/macro";

export const StyledAccordionRoot = styled.div<Pick<AccordionProps, "type">>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.type === "multiple" ? props.theme.mimirorg.spacing.base : props.theme.mimirorg.spacing.xs)};
  ${sizingMixin};
`;
