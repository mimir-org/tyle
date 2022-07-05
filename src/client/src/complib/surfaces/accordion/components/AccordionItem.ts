import { Item } from "@radix-ui/react-accordion";
import styled, { css } from "styled-components/macro";
import { focusRaw } from "../../../mixins";

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
  border-radius: ${(props) => props.theme.tyle.border.radius.medium};
  background-color: ${(props) => props.theme.tyle.color.sys.pure.base};

  :focus-within {
    position: relative;
    z-index: 1;
    ${focusRaw};
  }

  &[data-state="open"] {
    border: 1px solid ${(props) => props.theme.tyle.color.sys.secondary.base};
    background-color: ${(props) => props.theme.tyle.color.sys.secondary.container?.base};
  }

  ${({ disabled, ...props }) =>
    disabled &&
    css`
      pointer-events: none;
      color: ${props.theme.tyle.color.sys.surface.variant.on};
      background-color: ${props.theme.tyle.color.sys.outline.base};
    `};
`;
