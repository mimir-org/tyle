import { StoryFn } from "@storybook/react";
import { Box, Text } from "@mimirorg/component-library";
import { SettingsSection } from "features/settings/common/settings-section/SettingsSection";

export default {
  title: "Features/Settings/Common/SettingsSection",
  component: SettingsSection,
};

const Template: StoryFn<typeof SettingsSection> = (args) => <SettingsSection {...args}></SettingsSection>;

export const Default = Template.bind({});
Default.args = {
  title: "Your section title",
  children: (
    <Box>
      <Text>Your section content A</Text>
      <Text>Your section content B</Text>
      <Text>Your section content C</Text>
      <Text>Your section content D</Text>
    </Box>
  ),
};
