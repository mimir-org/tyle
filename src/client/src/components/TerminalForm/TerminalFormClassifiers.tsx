import { Token } from "@mimirorg/component-library";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { FormSection } from "../FormSection/FormSection";
import { XCircle } from "@styled-icons/heroicons-outline";
import { SelectItemDialog } from "../SelectItemDialog/SelectItemDialog";
import { TerminalFormFields, classifierInfoItem } from "./TerminalForm.helpers";
import { useGetClassifiers } from "api/classifier.queries";
import { BlockFormFields } from "../BlockForm/BlockForm.helpers";

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */

export const FormClassifiers = () => {
  const { t } = useTranslation("entities");

  type BlockOrTerminalFormFields = BlockFormFields | TerminalFormFields;

  const {
    control,
    register,
    formState: { errors },
  } = useFormContext<BlockOrTerminalFormFields>();

  const classifierFields = useFieldArray({ control, name: "classifiers" });
  const classifierQuery = useGetClassifiers();
  const classifierInfoItems = classifierQuery.data?.map((p) => classifierInfoItem(p)) ?? [];

  return (
    <FormSection
      title={t("terminal.classifiers.title")}
      error={errors.classifiers}
      action={
        <SelectItemDialog
          title={t("terminal.classifiers.dialog.title")}
          description={t("terminal.classifiers.dialog.description")}
          searchFieldText={t("terminal.classifiers.dialog.search")}
          addItemsButtonText={t("terminal.classifiers.dialog.add")}
          openDialogButtonText={t("terminal.classifiers.dialog.open")}
          items={classifierInfoItems}
          onAdd={(ids) => {
            ids.forEach((id) => {
              const targetClassifier = classifierQuery.data?.find((x) => x.id === Number(id));
              if (targetClassifier) classifierFields.append(targetClassifier);
            });
          }}
        />
      }
    >
      {classifierFields.fields.map((field, index) => (
        <Token
          variant={"secondary"}
          key={field.id}
          {...register(`classifiers.${index}`)}
          actionable
          actionIcon={<XCircle />}
          actionText={t("terminal.classifiers.remove")}
          onAction={() => classifierFields.remove(index)}
          dangerousAction
        >
          {field.name}
        </Token>
      ))}
    </FormSection>
  );
};
