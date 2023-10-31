// import { Aspect, ConnectorDirection } from "@mimirorg/typelibrary-types";
// import { Trash } from "@styled-icons/heroicons-outline";
// import { getOptionsFromEnum } from "common/utils/getOptionsFromEnum";
// import {
//   MAXIMUM_TERMINAL_QUANTITY_VALUE,
//   MINIMUM_TERMINAL_QUANTITY_VALUE,
// } from "common/utils/blockTerminalQuantityRestrictions";
// import {
//   Accordion,
//   AccordionContent,
//   AccordionItem,
//   AccordionTrigger,
//   Box,
//   Button,
//   Checkbox,
//   Counter,
//   Flexbox,
//   FormField,
//   Select,
//   Text,
// } from "@mimirorg/component-library";
// import { useGetTerminals } from "external/sources/terminal/terminal.queries";
// //import { TerminalButton } from "features/common/terminal";
// import {
//   BlockTerminalContainer,
//   BlockTerminalInputContainer,
// } from "features/entities/block/terminals/BlockTerminal.styled";
// import { BlockTerminalAttributes } from "features/entities/block/terminals/BlockTerminalAttributes";
// import { FormBlockLib } from "features/entities/block/types/formBlockLib";
// import { Control, Controller, FieldArrayWithId, FieldErrors, UseFormSetValue, useWatch } from "react-hook-form";
// import { useTranslation } from "react-i18next";
// import { useTheme } from "styled-components/macro";
// import { useEffect } from "react";

// interface BlockTerminalProps {
//   index: number;
//   control: Control<FormBlockLib>;
//   field: FieldArrayWithId<FormBlockLib, "blockTerminals">;
//   errors: FieldErrors<FormBlockLib>;
//   setValue: UseFormSetValue<FormBlockLib>;
//   removable: boolean;
//   onRemove: () => void;
//   minValue?: number;
// }

// /**
//  * Component which represents a single terminal for a given block.
//  * Displays the various input fields that the terminal model supports.
//  *
//  * @param index
//  * @param control
//  * @param field
//  * @param errors
//  * @param setValue
//  * @param removable
//  * @param onRemove
//  * @constructor
//  */
// export const BlockTerminal = ({
//   index,
//   control,
//   field,
//   errors,
//   setValue,
//   removable = true,
//   onRemove,
//   minValue,
// }: BlockTerminalProps) => {
//   const theme = useTheme();
//   const { t } = useTranslation("entities");

//   const terminalQuery = useGetTerminals({ staleTime: 60 * 1000 });
//   const connectorDirectionOptions = getOptionsFromEnum<ConnectorDirection>(ConnectorDirection);

//   const aspect = useWatch({ control, name: "aspect" });
//   const allTerminals = useWatch({ control, name: "blockTerminals" });
//   const terminalHasMaxQuantity = useWatch({ control, name: `blockTerminals.${index}.hasMaxQuantity` });
//   const terminalCanHaveLimit = aspect === Aspect.Product;

//   const directionOptions = (terminalId: string | undefined) => {
//     if (!terminalId) return connectorDirectionOptions;

//     return connectorDirectionOptions.filter(
//       (x) =>
//         !allTerminals
//           .filter((y) => y.terminalId === terminalId)
//           .map((y) => y.connectorDirection)
//           .includes(x.value),
//     );
//   };

//   const sourceTerminal = terminalQuery.data?.find((x) => x.id === allTerminals[index].terminalId);

//   useEffect(() => {
//     if (aspect === Aspect.Function) {
//       setValue(`blockTerminals.${index}.maxQuantity`, 0, {
//         shouldDirty: true,
//       });
//       setValue(`blockTerminals.${index}.hasMaxQuantity`, false, {
//         shouldDirty: true,
//       });
//     }
//   }, [index, setValue, aspect]);

//   return (
//     <Flexbox gap={"24px"} alignItems={"center"}>
//       <Text variant={"title-large"}>{index + 1}</Text>
//       <BlockTerminalContainer>
//         <BlockTerminalInputContainer>
//           <Controller
//             control={control}
//             name={`blockTerminals.${index}.terminalId`}
//             render={({ field: { value, onChange, ref, ...rest } }) => (
//               <FormField
//                 indent={false}
//                 label={t("block.terminals.name")}
//                 error={errors.blockTerminals?.[index]?.terminalId}
//               >
//                 <Select
//                   {...rest}
//                   selectRef={ref}
//                   placeholder={t("common.templates.select", { object: t("block.terminals.name").toLowerCase() })}
//                   options={terminalQuery.data?.filter(
//                     (x) => allTerminals.filter((y) => y.terminalId === x.id).length < connectorDirectionOptions.length,
//                   )}
//                   isLoading={terminalQuery.isLoading}
//                   getOptionLabel={(x) => x.name}
//                   getOptionValue={(x) => x.id.toString()}
//                   onChange={(x) => {
//                     onChange(x?.id);
//                     setValue(`blockTerminals.${index}.connectorDirection`, directionOptions(x?.id)[0].value);
//                   }}
//                   value={terminalQuery.data?.find((x) => x.id === value)}
//                   formatOptionLabel={(x) => (
//                     <Flexbox alignItems={"center"} gap={theme.mimirorg.spacing.base}>
//                       {/*x.color && <TerminalButton as={"span"} variant={"small"} color={x.color} />*/}
//                       <Text>{x.name}</Text>
//                     </Flexbox>
//                   )}
//                 />
//               </FormField>
//             )}
//           />
//           <Controller
//             control={control}
//             name={`blockTerminals.${index}.connectorDirection`}
//             render={({ field: { value, onChange, ref, ...rest } }) => (
//               <FormField
//                 indent={false}
//                 label={t("block.terminals.direction")}
//                 error={errors.blockTerminals?.[index]?.connectorDirection}
//               >
//                 <Select
//                   {...rest}
//                   selectRef={ref}
//                   placeholder={t("common.templates.select", {
//                     object: t("block.terminals.direction").toLowerCase(),
//                   })}
//                   options={directionOptions(allTerminals[index].terminalId)}
//                   onChange={(x) => onChange(x?.value)}
//                   value={connectorDirectionOptions.find((x) => x.value === value)}
//                   isDisabled={!removable}
//                 />
//               </FormField>
//             )}
//           />
//           {terminalCanHaveLimit && (
//             <BlockTerminalInputContainer>
//               <Controller
//                 control={control}
//                 name={`blockTerminals.${index}.hasMaxQuantity`}
//                 render={({ field: { onChange, value, ...rest } }) => (
//                   <FormField
//                     indent={false}
//                     label={t("block.terminals.limit")}
//                     error={errors.blockTerminals?.[index]?.hasMaxQuantity}
//                   >
//                     <Box display={"flex"} justifyContent={"center"} alignItems={"center"} height={"40px"}>
//                       <Checkbox
//                         {...rest}
//                         onCheckedChange={(checked) => {
//                           !checked &&
//                             setValue(`blockTerminals.${index}.maxQuantity`, 0, {
//                               shouldDirty: true,
//                             });
//                           checked &&
//                             setValue(`blockTerminals.${index}.maxQuantity`, minValue ?? 1, {
//                               shouldDirty: true,
//                             });
//                           onChange(checked);
//                         }}
//                         checked={value}
//                         disabled={!terminalCanHaveLimit}
//                       />
//                     </Box>
//                   </FormField>
//                 )}
//               />
//               <Controller
//                 control={control}
//                 name={`blockTerminals.${index}.maxQuantity`}
//                 render={({ field: { value, ...rest } }) => (
//                   <FormField
//                     indent={false}
//                     label={t("block.terminals.amount")}
//                     error={errors.blockTerminals?.[index]?.maxQuantity}
//                   >
//                     <Counter
//                       {...rest}
//                       id={field.id}
//                       min={minValue ?? MINIMUM_TERMINAL_QUANTITY_VALUE}
//                       max={MAXIMUM_TERMINAL_QUANTITY_VALUE}
//                       value={!terminalHasMaxQuantity ? 0 : value}
//                       disabled={!terminalHasMaxQuantity}
//                     />
//                   </FormField>
//                 )}
//               />
//             </BlockTerminalInputContainer>
//           )}
//         </BlockTerminalInputContainer>
//         {sourceTerminal && sourceTerminal.attributes.length >= 4 ? (
//           <Accordion>
//             <AccordionItem value={"attributes"}>
//               <AccordionTrigger>{t("block.terminals.attributes")}</AccordionTrigger>
//               <AccordionContent>
//                 <BlockTerminalAttributes hideLabel attributes={[] /*sourceTerminal?.attributes ?? []*/} />
//               </AccordionContent>
//             </AccordionItem>
//           </Accordion>
//         ) : (
//           <BlockTerminalAttributes hideLabel attributes={[] /*sourceTerminal?.attributes ?? []*/} />
//         )}
//       </BlockTerminalContainer>
//       <Box>
//         <Button variant={"outlined"} dangerousAction disabled={!removable} alignSelf={"end"} onClick={() => onRemove()}>
//           <Trash size={48} />
//         </Button>
//       </Box>
//     </Flexbox>
//   );
// };
