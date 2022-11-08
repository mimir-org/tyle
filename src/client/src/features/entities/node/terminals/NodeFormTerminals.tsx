import { FormSection } from "features/entities/common/form-section/FormSection";
import { NodeFormTerminalsAddButton } from "features/entities/node/terminals/NodeFormTerminalsAddButton";
import { NodeTerminal } from "features/entities/node/terminals/NodeTerminal";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { createEmptyFormNodeTerminalLib } from "features/entities/node/types/formNodeTerminalLib";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";

export const NodeFormTerminals = () => {
  const { t } = useTranslation();
  const { control, setValue, formState } = useFormContext<FormNodeLib>();
  const { errors } = formState;

  const terminalFields = useFieldArray({ control, name: "nodeTerminals" });

  return (
    <FormSection
      title={t("terminals.title")}
      error={errors.nodeTerminals}
      action={<NodeFormTerminalsAddButton onClick={() => terminalFields.append(createEmptyFormNodeTerminalLib())} />}
    >
      {terminalFields.fields.map((field, index) => (
        <NodeTerminal
          key={field.id}
          index={index}
          control={control}
          field={field}
          errors={errors}
          setValue={setValue}
          onRemove={() => terminalFields.remove(index)}
        />
      ))}
    </FormSection>
  );
};
