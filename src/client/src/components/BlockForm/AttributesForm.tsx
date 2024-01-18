import AddAttributesForm from "components/AddAttributesForm";
import { BlockFormStepProps } from "components/BlockForm/BlockForm";
import React from "react";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";

const AttributesForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields, setFields }, ref) => {
  const { attributes } = fields;
  const setAttributes = (attributes: AttributeTypeReferenceView[]) => setFields({ ...fields, attributes });

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <AddAttributesForm attributes={attributes} setAttributes={setAttributes} />
    </form>
  );
});

AttributesForm.displayName = "AttributesForm";

export default AttributesForm;
