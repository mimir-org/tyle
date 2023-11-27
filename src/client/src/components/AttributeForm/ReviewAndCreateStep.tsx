import { useCreateAttribute } from "api/attribute.queries";
import { onSubmitForm, useSubmissionToast } from "helpers/form.helpers";
import { useNavigateOnCriteria } from "hooks/useNavigateOnCriteria";
import React from "react";
import { AttributeFormStepProps } from "./AttributeForm";
import { toAttributeTypeRequest } from "./AttributeForm.helpers";
import ReviewAndSubmitStep from "./ReviewAndSubmitStep";

const ReviewAndCreateStep = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields }, ref) => {
  const mutation = useCreateAttribute();

  useNavigateOnCriteria("/", mutation.isSuccess);

  const toast = useSubmissionToast("attribute");

  const handleSubmit = (event: React.FormEvent) => {
    event.preventDefault();
    onSubmitForm(toAttributeTypeRequest(fields), mutation.mutateAsync, toast);
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <ReviewAndSubmitStep attributeFormFields={fields} />
    </form>
  );
});

ReviewAndCreateStep.displayName = "ReviewAndCreateStep";

export default ReviewAndCreateStep;
