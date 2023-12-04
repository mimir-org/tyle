import AddClassifiersForm from "components/AddClassifiersForm";
import { BlockFormStepProps } from "components/BlockForm/BlockForm";
import React from "react";
import { RdlClassifier } from "types/common/rdlClassifier";

const ClassifiersForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields, setFields }, ref) => {
  const { classifiers } = fields;
  const addClassifiers = (classifiersToAdd: RdlClassifier[]) =>
    setFields({ ...fields, classifiers: [...fields.classifiers, ...classifiersToAdd] });
  const removeClassifier = (classifierToRemove: RdlClassifier) =>
    setFields({ ...fields, classifiers: classifiers.filter((classifier) => classifier.id !== classifierToRemove.id) });

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <AddClassifiersForm
        classifiers={classifiers}
        addClassifiers={addClassifiers}
        removeClassifier={removeClassifier}
      />
    </form>
  );
});

ClassifiersForm.displayName = "ClassifiersForm";

export default ClassifiersForm;
