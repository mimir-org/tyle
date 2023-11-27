import { useUpdateAttribute } from "api/attribute.queries";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React from "react";
import { useParams } from "react-router-dom";
import { AttributeFormStepProps } from "./AttributeForm";
import { toAttributeTypeRequest } from "./AttributeForm.helpers";
import ReviewAndSubmitStep from "./ReviewAndSubmitStep";

const ReviewAndUpdateStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields }, ref) => {
  const { id } = useParams();

  if (id === undefined) throw new Error("Can't resolve id of attribute to update.");

  const mutation = useUpdateAttribute(id);

  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(fields), mutation.mutateAsync, toast);
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <ReviewAndSubmitStep attributeFormFields={fields} mode="edit" />
    </form>
  );
});

ReviewAndUpdateStep.displayName = "ReviewAndUpdateStep";

export default ReviewAndUpdateStep;
