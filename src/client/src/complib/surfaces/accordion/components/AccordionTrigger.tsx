import { ForwardedRef, forwardRef } from "react";
import { useTheme } from "styled-components";
import { Text } from "../../../text";
import { StyledAccordionChevron, StyledAccordionHeader, StyledAccordionTrigger } from "./AccordionTrigger.styled";

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
        <Text as={"span"} variant={"body-large"} color={theme.tyle.color.sys.pure.on}>
          {children}
        </Text>
        <StyledAccordionChevron color={theme.tyle.color.sys.primary.base} size={24} />
      </StyledAccordionTrigger>
    </StyledAccordionHeader>
  );
});

AccordionTrigger.displayName = "AccordionTrigger";
