import { useCreateAttribute } from "api/attribute.queries";
import React from "react";
import { AttributeFormStepProps } from "./AttributeForm";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndCreateForm = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields }, ref) => {
  const mutation = useCreateAttribute();

  return <ReviewAndSubmitForm attributeFormFields={fields} mutation={mutation} formRef={ref} />;
});

ReviewAndCreateForm.displayName = "ReviewAndCreateForm";

export default ReviewAndCreateForm;
