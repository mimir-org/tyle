// import { FormSection } from "features/entities/common/form-section/FormSection";
// import { BlockFormTerminalsAddButton } from "features/entities/block/terminals/BlockFormTerminalsAddButton";
// import { BlockTerminal } from "features/entities/block/terminals/BlockTerminal";
// import { useFieldArray, useFormContext } from "react-hook-form";
// import { useTranslation } from "react-i18next";
// import { BlockTerminalLibCm } from "@mimirorg/typelibrary-types";
// import { BlockFormFields } from "../BlockForm.helpers";

// interface BlockFormTerminalsProps {
//   canAddTerminals?: boolean;
//   limitedTerminals?: BlockTerminalLibCm[];
// }

// /**
//  * Form section for adding terminals to blocks
//  *
//  * @param canAddTerminals controls if the add action is shown
//  * @param limitedTerminals list of terminals that cannot be removed
//  * @constructor
//  */
// export const BlockFormTerminals = ({ canAddTerminals = true, limitedTerminals }: BlockFormTerminalsProps) => {
//   const { t } = useTranslation("entities");
//   const { control, setValue, formState } = useFormContext<BlockFormFields>();
//   const { errors } = formState;

//   const terminalFields = useFieldArray({ control, name: "blockTerminals" });

//   return (
//     <FormSection
//       title={t("block.terminals.title")}
//       error={errors.blockTerminals}
//       action={
//         canAddTerminals && (
//           <BlockFormTerminalsAddButton onClick={() => terminalFields.append(createEmptyFormBlockTerminalLib())} />
//         )
//       }
//     >
//       {terminalFields.fields.map((field, index) => (
//         <BlockTerminal
//           key={`${index + field.id}`}
//           index={index}
//           control={control}
//           field={field}
//           errors={errors}
//           setValue={setValue}
//           removable={
//             limitedTerminals
//               ?.map((x) => x.terminal.id + x.connectorDirection)
//               .includes(field.terminalId + field.connectorDirection)
//               ? false
//               : true
//           }
//           onRemove={() => terminalFields.remove(index)}
//           minValue={
//             limitedTerminals?.find(
//               (x) => x.terminal.id === field.terminalId && x.connectorDirection === field.connectorDirection,
//             )?.maxQuantity
//           }
//         />
//       ))}
//     </FormSection>
//   );
// };
