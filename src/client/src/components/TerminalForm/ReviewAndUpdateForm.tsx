import { useUpdateTerminal } from "../../api/terminal.queries";
import React from "react";
import { useParams } from "react-router-dom";
import { TerminalFormStepProps } from "./TerminalForm2";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndUpdateForm = React.forwardRef<HTMLFormElement, TerminalFormStepProps>(({ fields }, ref) => {
  const { id } = useParams();

  if (id === undefined) throw new Error("Can't resolve id of terminal to update.");

  const mutation = useUpdateTerminal(id);

  return <ReviewAndSubmitForm terminalFormFields={fields} mutation={mutation} formRef={ref} mode="edit" />;
});

ReviewAndUpdateForm.displayName = "ReviewAndUpdateForm";

export default ReviewAndUpdateForm;
