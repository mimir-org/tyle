import { ComponentStory } from "@storybook/react";
import { Box } from "./Box";
import { THEME } from "../core";

export default {
  title: "Layouts/Box",
  component: Box,
};

const Template: ComponentStory<typeof Box> = (args) => (
  <Box {...args}>
    {[...Array(3)].map((_, i) => {
      const color = 20 * i;
      return (
        <Box key={i} p={THEME.SPACING.XL} bgColor={`hsl(${color},60%,60%)`}>
          ITEM
        </Box>
      );
    })}
  </Box>
);

export const Default = Template.bind({});

export const WithDisplayFlex = Template.bind({});
WithDisplayFlex.args = {
  display: "flex",
};

export const WithPaddingAndBorder = Template.bind({});
WithPaddingAndBorder.args = {
  p: "16px",
  border: "2px solid red",
};

export const WithShadow = Template.bind({});
WithShadow.args = {
  boxShadow: THEME.SHADOW.BOX_MEDIUM,
};

export const WithPolymorphicAs = Template.bind({});
WithPolymorphicAs.args = {
  as: "section",
};
