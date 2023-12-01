import { useCreateBlock } from "api/block.queries";
import React from "react";
import { BlockFormStepProps } from "./BlockForm";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndUpdateForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields }, ref) => {
  const mutation = useCreateBlock();

  return <ReviewAndSubmitForm blockFormFields={fields} mutation={mutation} formRef={ref} />;
});

ReviewAndUpdateForm.displayName = "ReviewAndUpdateForm";

export default ReviewAndUpdateForm;
