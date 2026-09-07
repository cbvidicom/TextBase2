using System;
using System.Collections.Generic;
using System.Text;

namespace Textbase.Application.Features.ClientApplications;

public sealed record ClientApplicationReferenceCounts(
	Guid ClientApplicationGuid,
	int LocaleCount,
	int TextResourceCount
	);