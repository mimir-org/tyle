import { StoryFn } from "@storybook/react";
import { theme } from "complib/core";
import { Box } from "complib/layouts/Box";

export default {
  title: "Layouts/Box",
  component: Box,
};

const Template: StoryFn<typeof Box> = (args) => (
  <Box {...args}>
    {[...Array(3)].map((_, i) => {
      const color = 20 * i;
      return (
        <Box key={i} p={theme.spacing.xl} bgColor={`hsl(${color},60%,60%)`}>
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
  boxShadow: theme.shadow.medium,
};

export const WithPolymorphicAs = Template.bind({});
WithPolymorphicAs.args = {
  as: "section",
};
