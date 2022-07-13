import { ForwardedRef, forwardRef, ReactNode } from "react";
import { StyledAccordionContent } from "./AccordionContent.styled";

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
