import { useTheme } from "styled-components";
import { MotionCard } from "../../../../../../complib/surfaces";
import { Flexbox } from "../../../../../../complib/layouts";
import { ItemDescription } from "./ItemDescription";
import { SearchItem } from "../../../../types/SearchItem";
import { ItemActions } from "./ItemActions";
import { Node } from "../../../about/components/node/Node";

export type SearchItemProps = SearchItem & {
  isSelected?: boolean;
  setSelected?: () => void;
};

/**
 * Component which presents information about a given item.
 *
 * @param img source for preview image
 * @param color background for image container
 * @param name of item
 * @param description of item
 * @param isSelected controls the selected style of the item
 * @param setSelected function for setting the currently selected item
 * @constructor
 */
export const Item = ({ img, color, name, description, isSelected, setSelected }: SearchItemProps) => {
  const theme = useTheme();

  return (
    <MotionCard
      layout
      variant={isSelected ? "elevated" : "filled"}
      {...theme.tyle.animation.fade}
      {...theme.tyle.animation.selectHover}
    >
      <Flexbox justifyContent={"space-between"} alignItems={"center"} flexWrap={"wrap"} gap={theme.tyle.spacing.medium}>
        <Node color={color} img={img} />
        <ItemDescription title={name} description={description} onClick={setSelected} />
        <ItemActions />
      </Flexbox>
    </MotionCard>
  );
};
