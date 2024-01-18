import { Content, Header, Item, Trigger } from "@radix-ui/react-accordion";
import { ChevronDown } from "@styled-icons/heroicons-outline";
import { getTextRole } from "helpers/theme.helpers";
import { focusRaw, sizingMixin } from "styleConstants";
import styled, { css, keyframes } from "styled-components";
import { AccordionProps } from "./Accordion";

export const StyledAccordionRoot = styled.div<Pick<AccordionProps, "type">>`
  display: flex;
  flex-direction: column;
  gap: ${(props) => (props.type === "multiple" ? props.theme.tyle.spacing.base : props.theme.tyle.spacing.xs)};
  ${sizingMixin};
`;

const emptyState = css`
  padding-top: 0;
  padding-bottom: 0;
  opacity: 0;
  height: 0;
`;

const visibleState = css`
  opacity: 1;
  height: var(--radix-accordion-content-height);
`;

const slideDown = keyframes`
  from {
    ${emptyState}
  }
  to {
    ${visibleState}
  }
`;

const slideUp = keyframes`
  from {
    ${visibleState}
  }
  to {
    ${emptyState}
  }
`;

export const StyledAccordionContent = styled(Content)`
  overflow: hidden;
  color: ${(props) => props.theme.tyle.color.secondary.container?.on};
  padding: ${(props) => props.theme.tyle.spacing.xl};

  &[data-state="open"] {
    animation: ${slideDown} 300ms cubic-bezier(0.87, 0, 0.13, 1) forwards;
  }

  &[data-state="closed"] {
    animation: ${slideUp} 300ms cubic-bezier(0.87, 0, 0.13, 1) forwards;
  }

  ${getTextRole("body-large")};
`;

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
  background-color: ${(props) => props.theme.tyle.color.pure.base};

  :focus-within {
    position: relative;
    z-index: 1;
    ${focusRaw};
  }

  &[data-state="open"] {
    border: 1px solid ${(props) => props.theme.tyle.color.secondary.base};
    background-color: ${(props) => props.theme.tyle.color.secondary.container?.base};
  }

  ${({ disabled, ...props }) =>
    disabled &&
    css`
      pointer-events: none;
      color: ${props.theme.tyle.color.surface.variant.on};
      background-color: ${props.theme.tyle.color.outline.base};
    `};
`;

export const StyledAccordionHeader = styled(Header)`
  all: unset;
  display: flex;
`;

export const StyledAccordionChevron = styled(ChevronDown)`
  transition: transform 300ms cubic-bezier(0.87, 0, 0.13, 1);

  [data-state="open"] & {
    transform: rotate(180deg);
  }

  [data-state="closed"] & {
    transform: rotate(0deg);
  }

  path {
    stroke-width: 1.5;
  }
`;

export const StyledAccordionTrigger = styled(Trigger)`
  all: unset;
  flex: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: ${(props) => props.theme.tyle.spacing.l} ${(props) => props.theme.tyle.spacing.xl};

  :hover {
    text-decoration: underline;
  }
`;
