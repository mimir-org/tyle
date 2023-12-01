import { useUpdateBlock } from "api/block.queries";
import React from "react";
import { useParams } from "react-router-dom";
import { BlockFormStepProps } from "./BlockForm";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndUpdateForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields }, ref) => {
  const { id } = useParams();

  if (id === undefined) throw new Error("Can't resolve id of block to update.");

  const mutation = useUpdateBlock(id);

  return <ReviewAndSubmitForm blockFormFields={fields} mutation={mutation} formRef={ref} mode="edit" />;
});

ReviewAndUpdateForm.displayName = "ReviewAndUpdateForm";

export default ReviewAndUpdateForm;
