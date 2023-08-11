import { Item } from "@radix-ui/react-accordion";
import { focusRaw } from "complib/mixins";
import styled, { css } from "styled-components/macro";

/**
 * Container component for item(s) inside an accordion
 *
 * @example
 * <Accordion>
 *   <AccordionItem value={"item01-unique-value"}>
 *     <AccordionTrigger>Accordion label</AccordionTrigger>
 *     <AccordionContent>Accordion content</AccordionContent>
 *   </AccordionItem>
 * </Accordion>
 */
export const AccordionItem = styled(Item)`
  border: 1px solid transparent;
  border-radius: ${(props) => props.theme.mimirorg.border.radius.medium};
  background-color: ${(props) => props.theme.mimirorg.color.pure.base};

  :focus-within {
    position: relative;
    z-index: 1;
    ${focusRaw};
  }

  &[data-state="open"] {
    border: 1px solid ${(props) => props.theme.mimirorg.color.secondary.base};
    background-color: ${(props) => props.theme.mimirorg.color.secondary.container?.base};
  }

  ${({ disabled, ...props }) =>
    disabled &&
    css`
      pointer-events: none;
      color: ${props.theme.mimirorg.color.surface.variant.on};
      background-color: ${props.theme.mimirorg.color.outline.base};
    `};
`;
