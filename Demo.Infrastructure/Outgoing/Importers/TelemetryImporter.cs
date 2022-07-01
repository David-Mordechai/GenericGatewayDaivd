﻿using AeroCodeGenProtocols;
using Demo.Core.Interfaces.Outgoing;

namespace Demo.Infrastructure.Outgoing.Importers;

internal class TelemetryImporter : IOutgoingImporter
{
    public void Start(CancellationToken cancellationToken)
    {
        // Simulates UAV telemetry report
        _ = Task.Factory.StartNew(async () =>
        {
            var random = new Random();

            while (cancellationToken.IsCancellationRequested is false)
            {
                var value = random.Next(0, 10000) % 2 == 0;
                var gcsLightsRep = new GcsLightsRep(value, value);

                DataReady?.Invoke(this, gcsLightsRep);

                await Task.Delay(3000, cancellationToken);
            }
        }, cancellationToken);
    }

    public event EventHandler<object>? DataReady;
}