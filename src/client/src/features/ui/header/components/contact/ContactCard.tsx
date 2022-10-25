import { User } from "@styled-icons/heroicons-outline";
import { useTheme } from "styled-components";
import { Box, MotionBox } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";

interface ContactCardProps {
  name?: string;
  email?: string;
}

/**
 * Component showing contact details alongside a user icon
 *
 * @param name
 * @param email
 * @constructor
 */
export const ContactCard = ({ name, email }: ContactCardProps) => {
  const theme = useTheme();

  return (
    <MotionBox
      display={"flex"}
      justifyContent={"center"}
      alignItems={"center"}
      gap={theme.tyle.spacing.base}
      {...theme.tyle.animation.fade}
    >
      <User size={34} color={theme.tyle.color.sys.primary.base} />
      <Box maxWidth={"200px"}>
        <Text variant={"title-medium"}>{name}</Text>
        <Text as={"a"} href={`mailto:${email}`} variant={"title-small"}>
          {email}
        </Text>
      </Box>
    </MotionBox>
  );
};
