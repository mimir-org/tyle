import { FormSection } from "features/entities/common/form-section/FormSection";
import { AspectObjectFormTerminalsAddButton } from "features/entities/aspectobject/terminals/AspectObjectFormTerminalsAddButton";
import { AspectObjectTerminal } from "features/entities/aspectobject/terminals/AspectObjectTerminal";
import { FormAspectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { createEmptyFormAspectObjectTerminalLib } from "features/entities/aspectobject/types/formAspectObjectTerminalLib";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { AspectObjectTerminalLibCm } from "@mimirorg/typelibrary-types";

interface AspectObjectFormTerminalsProps {
  canAddTerminals?: boolean;
  limitedTerminals?: AspectObjectTerminalLibCm[];
}

/**
 * Form section for adding terminals to aspect objects
 *
 * @param canAddTerminals controls if the add action is shown
 * @param limitedTerminals list of terminals that cannot be removed
 * @constructor
 */
export const AspectObjectFormTerminals = ({
  canAddTerminals = true,
  limitedTerminals,
}: AspectObjectFormTerminalsProps) => {
  const { t } = useTranslation("entities");
  const { control, setValue, formState } = useFormContext<FormAspectObjectLib>();
  const { errors } = formState;

  const terminalFields = useFieldArray({ control, name: "aspectObjectTerminals" });

  return (
    <FormSection
      title={t("aspectObject.terminals.title")}
      error={errors.aspectObjectTerminals}
      action={
        canAddTerminals && (
          <AspectObjectFormTerminalsAddButton
            onClick={() => terminalFields.append(createEmptyFormAspectObjectTerminalLib())}
          />
        )
      }
    >
      {terminalFields.fields.map((field, index) => (
        <AspectObjectTerminal
          key={`${index},${field.id}`}
          index={index}
          control={control}
          field={field}
          errors={errors}
          setValue={setValue}
          removable={limitedTerminals?.map((x) => x.terminal.id).includes(field.terminalId) ? false : true}
          onRemove={() => terminalFields.remove(index)}
          minValue={limitedTerminals?.find((x) => x.terminal.id === field.terminalId)?.maxQuantity}
        />
      ))}
    </FormSection>
  );
};
