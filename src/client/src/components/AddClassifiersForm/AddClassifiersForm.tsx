import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetClassifiers } from "api/classifier.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import Token from "components/Token";
import { RdlClassifier } from "types/common/rdlClassifier";
import { InfoItem } from "types/infoItem";

interface AddClassifiersFormProps {
  classifiers: RdlClassifier[];
  addClassifiers: (classifiersToAdd: RdlClassifier[]) => void;
  removeClassifier: (classifierToRemove: RdlClassifier) => void;
}

const AddClassifiersForm = ({ classifiers, addClassifiers, removeClassifier }: AddClassifiersFormProps) => {
  const classifierQuery = useGetClassifiers();
  const classifierInfoItems: InfoItem[] =
    classifierQuery.data?.map((classifier) => ({
      id: classifier.id.toString(),
      name: classifier.name,
      descriptors: {
        Description: classifier.description,
        IRI: classifier.iri,
      },
    })) ?? [];

  const availableClassifiers = classifierInfoItems.filter(
    (classifier) =>
      classifiers.filter((selectedClassifier) => selectedClassifier.id.toString() === classifier.id).length === 0,
  );

  const handleAdd = (addedIds: string[]) => {
    const classifiersToAdd: RdlClassifier[] = [];
    addedIds.forEach((id) => {
      const targetClassifier = classifierQuery.data?.find((x) => x.id === Number(id));
      if (targetClassifier) classifiersToAdd.push(targetClassifier);
    });
    addClassifiers(classifiersToAdd);
  };

  return (
    <FormSection
      title="Add classifiers"
      action={
        <SelectItemDialog
          title="Select classifiers"
          description="You can select one or more classifiers"
          searchFieldText="Search"
          addItemsButtonText="Add classifiers"
          openDialogButtonText="Open add classifiers dialog"
          items={availableClassifiers}
          onAdd={handleAdd}
        />
      }
    >
      {classifiers.map((classifier) => (
        <Token
          variant={"secondary"}
          key={classifier.id}
          actionable
          actionIcon={<XCircle />}
          actionText="Remove classifier"
          onAction={() => removeClassifier(classifier)}
          dangerousAction
        >
          {classifier.name}
        </Token>
      ))}
    </FormSection>
  );
};

export default AddClassifiersForm;
