import { Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetClassifiers } from "api/classifier.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { RdlClassifier } from "types/common/rdlClassifier";
import { InfoItem } from "types/infoItem";

interface ClassifiersStepProps {
  chosenClassifiers: RdlClassifier[];
  setClassifiers: (classifiers: RdlClassifier[]) => void;
}

const ClassifiersStep = ({ chosenClassifiers, setClassifiers }: ClassifiersStepProps) => {
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

  const handleRemoveClassifier = (index: number) => {
    const nextClassifiers = [...chosenClassifiers];
    nextClassifiers.splice(index, 1);
    setClassifiers(nextClassifiers);
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
          items={classifierInfoItems.filter(
            (classifier) => chosenClassifiers.filter((chosen) => chosen.id.toString() === classifier.id).length === 0,
          )}
          onAdd={(ids) => {
            const classifiersToAdd: RdlClassifier[] = [];
            ids.forEach((id) => {
              const targetClassifier = classifierQuery.data?.find((x) => x.id === Number(id));
              if (targetClassifier) classifiersToAdd.push(targetClassifier);
            });
            setClassifiers([...chosenClassifiers, ...classifiersToAdd]);
          }}
        />
      }
    >
      {chosenClassifiers.map((classifier, index) => (
        <Token
          variant={"secondary"}
          key={classifier.id}
          actionable
          actionIcon={<XCircle />}
          actionText="Remove classifier"
          onAction={() => handleRemoveClassifier(index)}
          dangerousAction
        >
          {classifier.name}
        </Token>
      ))}
    </FormSection>
  );
};

export default ClassifiersStep;
