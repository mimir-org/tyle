import { useUpdateAttribute } from "api/attribute.queries";
import React from "react";
import { useParams } from "react-router-dom";
import { AttributeFormStepProps } from "./AttributeForm";
import ReviewAndSubmitForm from "./ReviewAndSubmitForm";

const ReviewAndUpdateForm = React.forwardRef<HTMLFormElement, AttributeFormStepProps>(({ fields }, ref) => {
  const { id } = useParams();

  if (id === undefined) throw new Error("Can't resolve id of attribute to update.");

  const mutation = useUpdateAttribute(id);

  return <ReviewAndSubmitForm attributeFormFields={fields} mutation={mutation} formRef={ref} mode="edit" />;
});

ReviewAndUpdateForm.displayName = "ReviewAndUpdateForm";

export default ReviewAndUpdateForm;
