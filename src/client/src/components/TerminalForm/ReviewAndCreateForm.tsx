import { useCreateTerminal } from "../../api/terminal.queries";
import React from "react";
import { TerminalFormStepProps } from "./TerminalForm";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndUpdateForm = React.forwardRef<HTMLFormElement, TerminalFormStepProps>(({ fields }, ref) => {
  const mutation = useCreateTerminal();

  return <ReviewAndSubmitForm terminalFormFields={fields} mutation={mutation} formRef={ref} />;
});

ReviewAndUpdateForm.displayName = "ReviewAndUpdateForm";

export default ReviewAndUpdateForm;
