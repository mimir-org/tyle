import { Root } from "@radix-ui/react-accordion";
import { ReactNode } from "react";
import { Sizing } from "../../props";
import { StyledAccordionRoot } from "./Accordion.styled";

export type AccordionProps = Sizing & {
  type?: "single" | "multiple";
  children: ReactNode;
};

/**
 * Component which can display a set of interactive headings that each can show or hide their associated section of content.
 *
 * See documentation link below for details.
 * @see https://www.radix-ui.com/docs/primitives/components/accordion
 *
 * @param type sets if more than one accordion can be open at the same time
 * @param delegated receives sizing props for overriding default styles
 * @param children
 * @constructor
 */
export const Accordion = ({ children, type = "single", ...delegated }: AccordionProps) => (
  <Root type={type} collapsible={true} asChild>
    <StyledAccordionRoot type={type} {...delegated}>
      {children}
    </StyledAccordionRoot>
  </Root>
);
