import { Root } from "@radix-ui/react-accordion";
import Text from "components/Text";
import { ForwardedRef, ReactNode, forwardRef } from "react";
import { useTheme } from "styled-components";
import { Sizing } from "types/styleProps";
import {
  StyledAccordionChevron,
  StyledAccordionContent,
  StyledAccordionHeader,
  StyledAccordionRoot,
  StyledAccordionTrigger,
} from "./Accordion.styled";

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
const Accordion = ({ children, type = "single", ...delegated }: AccordionProps) => (
  <Root type={type} collapsible={true} asChild>
    <StyledAccordionRoot type={type} {...delegated}>
      {children}
    </StyledAccordionRoot>
  </Root>
);

export default Accordion;

interface AccordionContentProps {
  children: ReactNode;
}

/**
 * Container component for content inside an accordion item.
 * Can contain both simple strings and custom JSX.
 *
 * @example
 * <Accordion>
 *   <AccordionItem value={"item01-unique-value"}>
 *     <AccordionTrigger>Accordion label</AccordionTrigger>
 *     <AccordionContent>Contents of the accordion item</AccordionContent>
 *   </AccordionItem>
 * </Accordion>
 */
export const AccordionContent = forwardRef((props: AccordionContentProps, ref: ForwardedRef<HTMLDivElement>) => {
  const { children, ...delegated } = props;

  return (
    <StyledAccordionContent {...delegated} ref={ref}>
      {children}
    </StyledAccordionContent>
  );
});

AccordionContent.displayName = "AccordionContent";

interface AccordionTriggerProps {
  children?: string;
}

/**
 * Trigger for an accordion item which handles the opening/closing state
 *
 * @example
 * <Accordion>
 *   <AccordionItem value={"item01-unique-value"}>
 *     <AccordionTrigger>Clicking me will open the content beneath</AccordionTrigger>
 *     <AccordionContent>Content beneath trigger</AccordionContent>
 *   </AccordionItem>
 * </Accordion>
 */
export const AccordionTrigger = forwardRef((props: AccordionTriggerProps, ref: ForwardedRef<HTMLButtonElement>) => {
  const theme = useTheme();
  const { children, ...delegated } = props;

  return (
    <StyledAccordionHeader>
      <StyledAccordionTrigger {...delegated} ref={ref}>
        <Text as={"span"} variant={"body-large"} color={theme.tyle.color.pure.on}>
          {children}
        </Text>
        <StyledAccordionChevron color={theme.tyle.color.primary.base} size={24} />
      </StyledAccordionTrigger>
    </StyledAccordionHeader>
  );
});

AccordionTrigger.displayName = "AccordionTrigger";
