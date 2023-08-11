import { User } from "@styled-icons/heroicons-outline";
import { Box, MotionBox, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

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
      gap={theme.mimirorg.spacing.base}
      {...theme.mimirorg.animation.fade}
    >
      <User size={34} color={theme.mimirorg.color.primary.base} />
      <Box maxWidth={"200px"}>
        <Text variant={"title-medium"}>{name}</Text>
        <Text color={theme.mimirorg.color.primary.base} as={"a"} href={`mailto:${email}`} variant={"title-small"}>
          {email}
        </Text>
      </Box>
    </MotionBox>
  );
};
