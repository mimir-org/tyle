import { Icon } from "../../../../../../complib/media";
import { Box } from "../../../../../../complib/layouts";
import { useTheme } from "styled-components";

export interface NodeProps {
  color: string;
  img: string;
  width?: string;
  height?: string;
  imgSize?: number;
}

/**
 * Component which serves as the visual representation for a node.
 *
 * @param color
 * @param img
 * @param width
 * @param height
 * @param imgSize
 * @constructor
 */
export const Node = ({ color, img, width = "130px", height = "100px", imgSize = 40 }: NodeProps) => {
  const theme = useTheme();

  return (
    <Box
      display={"flex"}
      justifyContent={"center"}
      alignItems={"center"}
      height={height}
      width={width}
      bgColor={color}
      border={`1px solid ${theme.tyle.color.outline.base}`}
      borderRadius={theme.tyle.border.radius.medium}
    >
      {img && <Icon size={imgSize} src={img} alt="" />}
    </Box>
  );
};
