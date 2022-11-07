import { FormSection } from "features/entities/common/form-section/FormSection";
import { createEmptyNodeTerminalLibAm } from "features/entities/node/terminals/NodeFormTerminals.helpers";
import { NodeFormTerminalsAddButton } from "features/entities/node/terminals/NodeFormTerminalsAddButton";
import { NodeTerminal } from "features/entities/node/terminals/NodeTerminal";
import { FormNodeLib } from "features/entities/node/types/formNodeLib";
import { useFieldArray, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";

export const NodeFormTerminals = () => {
  const { t } = useTranslation();
  const { control, formState } = useFormContext<FormNodeLib>();
  const { errors } = formState;

  const terminalFields = useFieldArray({ control, name: "nodeTerminals" });

  return (
    <FormSection
      title={t("terminals.title")}
      error={errors.nodeTerminals}
      action={<NodeFormTerminalsAddButton onClick={() => terminalFields.append(createEmptyNodeTerminalLibAm())} />}
    >
      {terminalFields.fields.map((field, index) => (
        <NodeTerminal
          key={field.id}
          index={index}
          control={control}
          field={field}
          errors={errors}
          onRemove={() => terminalFields.remove(index)}
        />
      ))}
    </FormSection>
  );
};
