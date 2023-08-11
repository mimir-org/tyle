import { Trash } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Box, Flexbox, Text } from "@mimirorg/component-library";
import { FieldsCardContainer } from "features/entities/common/fields-card/FieldsCard.styled";
import { ReactNode } from "react";
import { useTheme } from "styled-components";

interface FieldsCardProps {
  index: number;
  removeText: string;
  onRemove: () => void;
  children?: ReactNode;
}

/**
 * Component shows a card with a number and a remove button. The children of the component are listed vertically.
 * Multiple FieldsCard components are meant to be used together to represent collections of inputs.
 *
 * @param index number shown on card
 * @param removeText text for screen-readers
 * @param onRemove action when clicking the remove button
 * @param children
 * @constructor
 */
export const FieldsCard = ({ index, removeText, onRemove, children }: FieldsCardProps) => {
  const theme = useTheme();

  return (
    <FieldsCardContainer>
      <Box display={"flex"} justifyContent={"space-between"} width={"100%"}>
        <Text variant={"title-medium"}>#{index}</Text>
        <Button variant={"outlined"} icon={<Trash />} iconOnly onClick={() => onRemove()}>
          {removeText}
        </Button>
      </Box>

      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.s}>
        {children}
      </Flexbox>
    </FieldsCardContainer>
  );
};
